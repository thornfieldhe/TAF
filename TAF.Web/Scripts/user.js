var item = Vue.extend({
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
        'onUpdateItem': function (title,id) {
            this.get("/Home/GetUser?userId="+id);
        }
    },
    methods: {
        clearItem:function() {
            this.item.Id = "";
            this.item.LoginName = "";
            this.item.FullName = "";
            this.item.RoleIds = [];
        },
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
    ready: function () {
        $("#searchRoleId").select2().on("change", function (e) { main.queryEntity.roleId = $("#searchRoleId").val(); });
        this.query(1);
    },
    data: {
        queryEntity: {
            loginName: "",
            fullName: "",
            roleId:""
        },
        list: {},
        queryUrl : "/Home/GetUserList"
    },
    components: {
        'form-edit': item
    },
    events: {
        'onResetSearch': function () {
            this.queryEntity.loginName = "";
            this.queryEntity.fullName = "";
            this.queryEntity.roleId = "";
            $("#searchRoleId").select2().val("").trigger("change");
        }
    }
});
