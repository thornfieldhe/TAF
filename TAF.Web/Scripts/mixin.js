var indexMixin = {
    el: "#main",
    events: {
        'onAddItem': function (title) {
            this.$broadcast("onAddItem", title);
        },
        'onUpdateItem': function (title, id) {
            this.$broadcast("onUpdateItem", title, id);
        },
        'onDeleteItem':function(name, id) {
            this.$broadcast("onDeleteItem", name, id);
        },
        'onChange': function () {
            this.query(1);
        }
    },
    methods: {
        query: function (index) {
            var $this = this;
            $.get($this.queryUrl + "?pageSize=2&pageIndex=" + index, function (e) {
                $this.list = e.Data;
                $this.$broadcast("onQuery", $this.list);
            });
        }
    }
}


var itemMixin = {
    template: '#formBody',
    props: ['id'],
    ready: function () {
        this.validate();
    },
    events: {
        'onNewItem':function() {
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
                        $this.$dispatch('onChange');
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
            });
        }
    }
}
