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

        }
    },
    methods: {
        formInit: function (index) {
            this.validate();
            $('#addItemModal').on('hide.bs.modal', function () {
                $(form).data('bootstrapValidator').resetForm();
                $("#unknownError").show().find(".help-block").html();
                main.$children[index].item = {};
            });
        }
    },
    components: {
        'form-edit': {
            props: ['id', 'title'],
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
                submitForm: function (model) {
                    console.log(model,e,111);
                    if (e.Status === 0) {
                        model.$dispatch('postSaveItem');
                        $("#addItemModal").modal("hide");
                    } else {
                        $("#unknownError").show().find(".help-block").html(e.Message);
                    }
                }
            }
        }
    }
}
