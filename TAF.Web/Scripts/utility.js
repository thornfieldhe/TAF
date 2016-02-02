//taf基础工具
var taf= {
    //订阅器
    subscriber: {
        //新增订阅
        addSubscriber: function(id, callBack) {
            erp.subscriber.subscribers[id] = callBack;
        },
        //注销订阅
        unSubscriber: function(id) {
            delete erp.subscriber.subscribers[id];
        },
        //发布订阅
        publish: function(id, data) {
            var item = erp.subscriber.subscribers[id];
            if (item == null || item === 'undefined') return;
            item(data);
        }
    },
    //通知
    notify: {
        danger: function(text) {
            Notify(text, 'top-right', '5000', 'danger', 'fa-times', true);
        },
        success: function(text) {
            Notify(text, 'top-right', '5000', 'success', 'fa-times', true);
        }
    }
}