var indexMixin = {
    el: "#main",
    events: {
        'onAddItem': function (title) {
            this.clearForm();
            this.$broadcast("onAddItem", title);
        },
        'onUpdateItem': function (title, id) {
            this.clearForm();
            this.$broadcast("onUpdateItem", title, id);
        },
        'postSaveItem': function () {
            //todos 刷新列表
        }
    }
}

var itemMixin = {
    template: '#formEdit',
    props: ['id', 'title'],
    ready: function () {
        this.validate();
        $('#addItemModal').on('hide.bs.modal', function () {
            $(form).data('bootstrapValidator').resetForm();
            $("#unknownError").show().find(".help-block").html("");
            this.item = {};
        });
    },
    events: {
        'onAddItem': function (title) {
            this.title = title;
        },
        'onUpdateItem': function (title, id) {
            this.title = title;
            this.item.id = id;
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
                        $this.$dispatch('postSaveItem');
                    } else {
                        $("#unknownError").show().find(".help-block").html(e.Message);
                    }
                });
            }
        }
    }
}
