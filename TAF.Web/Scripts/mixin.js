var indexMixin = {
    el: "#main",
    events: {
        'onAddItem': function (title) {
            $("#addItemModal").modal("show");
            this.$broadcast("onAddItem", title);
        },
        'onUpdateItem': function (title, id) {
            $("#addItemModal").modal("show");
            this.$broadcast("onUpdateItem", title, id);
        },
        'onDeleteItem':function(name, id) {
            this.$broadcast("onDeleteItem", name, id);
        },
        'onChange': function (index) {
            this.query(index);
        }
    },
    methods: {
        query: function (index) {
            this.preQuery();
            var $this = this;
            $.get($this.queryUrl + "?pageSize=20&pageIndex=" + index, $this.queryEntity , function (e) {
                $this.list = e.Data;
                $this.$broadcast("onQuery", $this.list);
            });
        },
        preQuery:function() {
            
        }
    }
}


var itemMixin = {
    props: ['id'],
    ready: function () {
        this.validate();
    },
    events: {
        'onNewItem': function () {
            this.clearItem();
            this.editModel = false;
        }
    },
    methods: {
        submit: function (url) {
            var $this = this;
            $(form).data('bootstrapValidator').validate();
            if ($(form).data('bootstrapValidator').isValid()) {
                $.post(url, $this.item, function (e) {
                    if (e.Status === 0) {
                        $("#addItemModal").modal("hide");
                        $this.$dispatch('onChange',1);
                    } else {
                        $("#unknownError").show().find(".help-block").html(e.Message);
                    }
                });
            }
        },
        get: function (url) {
            var $this = this;
            $.get(url, function (e) {
                $this.item = e.Data;
                $this.postGet();
            });
        },
        postGet:function() {
            
        }
    }
}

//日期选择器配置
var datepickerConfig = {
    "autoApply": true,
    "minDate": "01/01/2000",
    "opens": "left",
    "locale": {
        "format": "YYYY/MM/DD",
        "separator": " - ",
        "daysOfWeek": [
            "日",
            "一",
            "二",
            "三",
            "四",
            "五",
            "六"
        ],
        "monthNames": [
            "一月",
            "二月",
            "三月",
            "四月",
            "五月",
            "六月",
            "七月",
            "八月",
            "九月",
            "十月",
            "十一月",
            "十二月"
        ],
        "firstDay": 1
    }
}

