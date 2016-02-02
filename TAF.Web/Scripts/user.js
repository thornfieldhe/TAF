var main = new Vue({
    el: "#main",
    data: {
        query: {
            liginName: "",
            fullName: "",
            roleNames:""
        }
    },
    components: {
        'form-edit': {
            props: ['model.Id', 'title'],
            template: '#formEdit',
            events: {
                'onAddItem': function (title) {
                    this.title = title;
                },
                'onUpdateItem': function (title, id) {
                    this.title = title;
                    this.id = id;
                }
            }
        }
    },
    events: {
        'onAddItem': function (title) {
            this.formInit();
            this.$broadcast("onAddItem", title);
        },
        'onUpdateItem': function (title,id) {
            this.formInit();
            this.$broadcast("onUpdateItem", title,id);
        }
    },
    methods: {
        validate: function () {
            $("#form").bootstrapValidator({
                message: '用户验证未通过',
                fields: {
                    loginName: {
                        validators: {
                            notEmpty: {
                                message: '用户名不能为空'
                            }
                        }
                    },
                    fullName: {
                        validators: {
                            notEmpty: {
                                message: '全名不能为空'
                            }
                        }
                    }
                }
            });
        },
        formInit: function () {
            this.validate();
        }
    }
});