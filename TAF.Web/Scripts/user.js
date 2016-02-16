Vue.component('form-edit', {
    mixins: [itemMixin],
    template: '#formEdit',
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
    methods: {
        saveItem: function () {
            this.submit("/Home/SaveUser");
        },
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
