var main = new Vue({
    el: "#main",
    data: {
        query: {
            liginName: "",
            fullName: "",
            roleNames:""
        }, checkedNames:[]
    },
    components: {
        'form-edit': {
            props: ['id', 'title'],
            template: '#formEdit',
            data: {
                item: {
                    id: '',
                    loginName: '',
                    fullName: '',
                    roleIds:[]
                }
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
                saveItem:function() {

                }
            }
        }
    },
    events: {
        'onAddItem': function (title,item) {
            this.formInit();
            console.log(item,3);
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
        },
        init:function() {
           
        }
    }
});
