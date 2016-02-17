var indexMixin = {
    el: "#main",
    events: {
        'onAddItem': function (title) {
            this.$broadcast("onAddItem", title);
        },
        'onUpdateItem': function (title, id) {
            this.$broadcast("onUpdateItem", title, id);
        },
        'postSaveItem': function () {
            //todos 刷新列表
        }
    }
}


var itemMixin = {
    template: '#formBody',
    props: ['id'],
    ready: function () {
        this.validate();
    },
    methods: {
        submit: function (url) {
            var $this = this;
            $(form).data('bootstrapValidator').validate();
            if ($(form).data('bootstrapValidator').isValid()) {
                $.post(url, $this.item, function (e) {
                    if (e.Status === 0) {
                        $("#addItemModal").modal("hide");
                        $this.$dispatch('postSaveItem');
                    } else {
                        $("#unknownError").show().find(".help-block").html(e.Message);
                    }
                });
            }
        }
    }
}
