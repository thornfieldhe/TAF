var main = new Vue({
    mixins: [indexMixin],
    data: {
        query: {
            liginName: "",
            fullName: "",
            roleNames:""
        }
    },
    components: {
        'form-edit': {
            template: '#formEdit',
            data: function () {
             return  { item: {
                    id: '',
                    loginName: '',
                    fullName: '',
                    roleIds:[]
                }};
            },
            methods: {
                saveItem: function (item) {
                    var $this = this;
                    console.log(this);
                    $(form).data('bootstrapValidator').validate();
                    if ($(form).data('bootstrapValidator').isValid()) {
                        $.post("/Home/SaveUser", item, function (e) {
                            if (e.Status === 0) {
                                $this.$dispatch('postSaveItem');
                                $("#addItemModal").modal("hide");
                            } else {
                                $("#unknownError").show().find(".help-block").html(e.Message);
                            }
                        });
                    }
                }
            }
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
        clearForm:function() {//初始化表单
            this.formInit(1);//1指vue子组件中的编辑组件
        }
    }
});
