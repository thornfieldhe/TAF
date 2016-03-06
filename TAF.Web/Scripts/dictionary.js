﻿var item1 = Vue.extend({
    mixins: [itemMixin],
    data: function () {
        return {
            item: {
                Id: '00000000-0000-0000-0000-000000000000',
                Key: '',
                Value: '',
                Value1: "",
                Value2: "",
                Value3: "",
                Value4: "",
                Value5: "",
                Value6: "",
                Value7: "",
                Value8: "",
                Value9: "",
            }
        };
    },
    events: {
        'onUpdateItem': function (title,id) {
            this.get("/Dictionary/Get?id=" + id);
        }
    },
    methods: {
    clearItem:function() {
        this.item.Id = "00000000-0000-0000-0000-000000000000";
        this.item.Key = "";
        this.item.Value = "";
        this.item.Value1= "";
        this.item.Value2 = "";
        this.item.Value3 = "";
        this.item.Value4 = "";
        this.item.Value5 = "";
        this.item.Value6 = "";
        this.item.Value7 = "";
        this.item.Value8 = "";
        this.item.Value9 = "";
    },
    saveItem: function () {
        this.submit("/Dictionary/Save");
    },
        validate: function () {
            $("#form").bootstrapValidator({
                message: '字典验证未通过',
                fields: {
                    key: {
                        validators: {
                            notEmpty: {
                                message: '键不能为空'
                            }
                        }
                    },
                    value: {
                        validators: {
                            notEmpty: {
                                message: '值不能为空'
                            }
                        }
                    }
                }
            });
        }
    }
});

var main1 = new Vue({
    mixins: [indexMixin],
    ready: function () {
//        $("#searchRoleId").select2().on("change", function (e) { main.queryEntity.roleId = $("#searchRoleId").val(); });
        this.query(1);
    },
    data: {
        queryEntity: {
            key: "",
            value: "",
            value1: "",
            value2: "",
            value3: "",
            value4: "",
            value5: "",
            value6: "",
            value7: "",
            value8: "",
            value9: "",
        },
        list: {},
        queryUrl : "/Dictionary/GetList"
    },
    components: {
        'form-edit': item1
    },
    events: {
        'onResetSearch': function () {
            this.queryEntity.key = "";
            this.queryEntity.value = "";
            this.queryEntity.value1 = "";
            this.queryEntity.value2 = "";
            this.queryEntity.value3= "";
            this.queryEntity.value4 = "";
            this.queryEntity.value5 = "";
            this.queryEntity.value6 = "";
            this.queryEntity.value7 = "";
            this.queryEntity.value8 = "";
            this.queryEntity.value9 = "";
//            $("#searchRoleId").select2().val("").trigger("change");
        }
    }
});
