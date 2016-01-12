namespace TAF.Core
{
    using System;
    using System.Collections.Generic;

    public abstract class CoRHandlerBase<Request>
    {
        protected CoRHandlerBase()
        {
            if (this.Successors == null)
            {
                Successors = new List<CoRHandlerBase<Request>>();
            }
        }

        public abstract bool AllowProcess(Request request);
        public abstract void Excute(Request request);

        public void Process(Request request)
        {
            if (AllowProcess(request))
            {
                Excute(request);
            }
        }

        /// <summary>
        /// 处理客户请求
        /// </summary>
        /// <param name="request"></param>
        public virtual void HandleRequest(Request request)
        {
            if (HasBreakPoint)
            {
                OnBreak(new CallHandlerEventArgs<Request>(this, request));
            }
            if (request == null)
            {
                return;
            }
            Process(request);

            if (Successors != null)
            {
                foreach (CoRHandlerBase<Request> successor in Successors)
                {
                    successor.HandleRequest(request);
                }
            }
        }

        public virtual void OnBreak(CallHandlerEventArgs<Request> argus)
        {
            this.Break?.Invoke(this, argus);
        }

        /// <summary>
        /// 添加后续节点
        /// </summary>
        /// <param name="success"></param>
        public void AddSuccessor(CoRHandlerBase<Request> success)
        {
            if (this.Successors == null)
            {
                Successors = new List<CoRHandlerBase<Request>>();
            }
            Successors.Add(success);
        }

        /// <summary>
        /// 实现迭代器，并且对容器对象实现隐性递归
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CoRHandlerBase<Request>> Enumerate()
        {
            yield return this;
            if ((Successors != null) && (Successors.Count > 0))
                foreach (CoRHandlerBase<Request> child in Successors)
                {
                    foreach (CoRHandlerBase<Request> item in child.Enumerate())
                        yield return item;
                }
        }

        /// <summary>
        /// 后继节点
        /// </summary>
        public List<CoRHandlerBase<Request>> Successors
        {
            get; set;
        }

        /// <summary>
        /// 是否定义断点
        /// </summary>
        public bool HasBreakPoint
        {
            get; set;
        }

        /// <summary>
        /// 断点事件
        /// </summary>
        public event EventHandler<CallHandlerEventArgs<Request>> Break;


    }
}