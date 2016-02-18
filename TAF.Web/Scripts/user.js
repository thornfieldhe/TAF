Vue.component('form-body', {
    mixins: [itemMixin],
    data: function () {
        return {
            item: {
                Id: '',
                LoginName: '',
                FullName: '',
                RoleIds: []
            },
            editModel: false
        };
    },
    events: {
        'onSaveItem': function () {
            this.submit("/Home/SaveUser");
        },
        'onClearItem': function () {
            this.item.Id = "";
            this.item.LoginName = "";
            this.item.FullName = "";
            this.item.RoleIds = [];
        },
        'onGetItem': function (id) {
            this.editModel = true;
            this.get("/Home/GetUser?userId="+id);
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
    ready: function () {
        this.query(1);
    },
    data: {
        queryEntity: {
            liginName: "",
            fullName: "",
            roleNames:""
        },
        list: {},
        queryUrl : "/Home/GetUserList"
    }
});
