namespace TAF.Test.Business
{

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using TAF.Core;

    /// <summary>
    /// CorTest 的摘要说明
    /// </summary>
    [TestClass]
    public class CorTest
    {
        /// <summary>
        /// 顺序执行
        /// </summary>
        [TestMethod]
        public void CorTest1()
        {
            CoRHandlerBase<Request> handler1 = new InternalHandler();
            CoRHandlerBase<Request> handler2 = new MailHandler();
            CoRHandlerBase<Request> handler3 = new DiscountHandler();
            CoRHandlerBase<Request> handler5 = new Regular2Handler();

            var request = new Request(20, "Mail");
            handler1.AddSuccessor(handler3);
            handler3.AddSuccessor(handler2);
            handler2.AddSuccessor(handler5);
            handler1.HandleRequest(request);
            Assert.AreEqual(20 * 1.3 * 10, request.Price);

        }

        /// <summary>
        /// 熔断短路
        /// </summary>
        [TestMethod]
        public void CorTest2()
        {
            CoRHandlerBase<Request> handler1 = new InternalHandler();
            CoRHandlerBase<Request> handler2 = new MailHandler();
            CoRHandlerBase<Request> handler3 = new DiscountHandler();
            handler3.HasBreakPoint = true;
            handler3.Break += Handler1_Break;

            var request = new Request(20, "Mail");
            handler1.HandleRequest(request);
            handler1.AddSuccessor(handler3);
            handler3.AddSuccessor(handler2);
            Assert.AreEqual<double>(20, request.Price);
        }

        /// <summary>
        /// 链式执行
        /// </summary>
        [TestMethod]
        public void CorTest3()
        {
            CoRHandlerBase<Request> handler1 = new InternalHandler();
            CoRHandlerBase<Request> handler2 = new MailHandler();
            CoRHandlerBase<Request> handler3 = new DiscountHandler();
            CoRHandlerBase<Request> handler4 = new RegularHandler();
            CoRHandlerBase<Request> handler5 = new Regular2Handler();

            var request = new Request(20, "Mail");
            handler1.AddSuccessor(handler3);
            handler3.AddSuccessor(handler2);
            handler3.AddSuccessor(handler5);
            handler3.AddSuccessor(handler4);
            handler1.HandleRequest(request);
            Assert.AreEqual<double>(20 * 1.3 * 10 * 5, request.Price);

        }

        public static void Handler1_Break(object sender, CallHandlerEventArgs<Request> e)
        {
            var handler = e.Handler;
            handler.HasBreakPoint = false;
            handler.Successors = null;
            handler.HandleRequest(e.Request);
        }

    }

    public class InternalHandler : CoRHandlerBase<Request>
    {
        public override void Excute(Request request)
        {
            request.Price *= 0.6;
        }

        public override bool AllowProcess(Request request)
        {
            return "Internal" == request.Contex;
        }
    }

    public class MailHandler : CoRHandlerBase<Request>
    {
        public override void Excute(Request request)
        {
            request.Price *= 1.3;
        }

        public override bool AllowProcess(Request request)
        {
            return request.Contex == "Mail";
        }
    }

    public class DiscountHandler : CoRHandlerBase<Request>
    {
        public override void Excute(Request request)
        {
            request.Price *= 0.9;
        }

        public override bool AllowProcess(Request request)
        {
            return request.Contex == "Discount";
        }
    }

    public class RegularHandler : CoRHandlerBase<Request>
    {
        public override void Excute(Request request)
        {
            request.Price *= 5;
        }

        public override bool AllowProcess(Request request)
        {
            return request.Contex.StartsWith("M");
        }
    }

    public class Regular2Handler : CoRHandlerBase<Request>
    {
        public override void Excute(Request request)
        {
            request.Price *= 10;
        }

        public override bool AllowProcess(Request request)
        {
            return request.Contex.Contains("Ma");
        }
    }

    public class Request 
    {
        public double Price
        {
            get; set;
        }
        public string Contex
        {
            get; set;
        }

        public Request(double price, string type)
        {
            this.Price = price;
            this.Contex = type;
        }
    }

}