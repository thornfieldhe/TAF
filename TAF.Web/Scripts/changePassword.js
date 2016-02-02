var main = new Vue({
    el: "#main",
    methods: {
        submit:function() {
            $(form).data('bootstrapValidator').validate();
            if ($(form).data('bootstrapValidator').isValid()) {
                $.post("/Home/ChangePassword", main.$data, function (e) {
                    if (e.Status === 0) {
                        taf.notify.success(e.Data);
                    } else {
                        taf.notify.danger(e.Message);
                    }
                });
            }
        },
        init:function() {
            $("#form").bootstrapValidator({
                message: '数据验证未通过',
                fields: {
                    currentPassword: {
                        validators: {
                            notEmpty: {
                                message: '当前密码不能为空'
                            }
                        }
                    },
                    newPassword: {
                        validators: {
                            notEmpty: {
                                message: '新密码不能为空'
                            },
                            stringLength: {
                                min: 6,
                                message: '密码不能少于6位'
                            },
                            identical: {
                                field: 'confirmPassword',
                                message: '新密码与确认密码不一致'
                            }
                        }
                    },
                    confirmPassword: {
                        validators: {
                            notEmpty: {
                                message: '确认密码不能为空'
                            },
                            identical: {
                                field: 'newPassword',
                                message: '新密码与确认密码不一致'
                            }
                        }
                    }
                }
            });
        }
    }
});

main.init();