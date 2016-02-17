Vue.component('form-body', {
    mixins: [itemMixin],
    data: function () {
        return {
            item: {
                id: '',
                loginName: '',
                fullName: '',
                roleIds: []
            }
        };
    },
    events: {
        'onSaveItem': function (id) {
            this.item.id = id;
            this.submit("/Home/SaveUser");
        },
        'onClearItem': function () {
            this.item.id = "";
            this.item.loginName = "";
            this.item.fullName = "";
            this.item.roleIds = [];
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
        }
    }
});

var main = new Vue({
    mixins: [indexMixin],
    data: {
        query: {
            liginName: "",
            fullName: "",
            roleNames:""
        }
    }
});
