Vue.component('form-body', {
    mixins: [itemMixin],
    template: '#productFormBody',
    data: function () {
        return {
            item: {
                Id: '00000000-0000-0000-0000-000000000000',
                Name: '',
                CategoryId: '',
                ColorId: "",
                Price: "",
                ProductionDate: ""
            }
        };
    },
    events: {
        'onSaveItem': function () {
            this.submit("/Product/Save");
        },
        'onGetItem': function (id) {
            this.get("/Product/Get?id=" + id);
        }
    },
    methods: {
        clearItem: function () {
            this.item.Id='00000000-0000-0000-0000-000000000000';
            this.item.Name = '';
            this.item.CategoryId='';
            this.item.ColorId = "";
            this.item.Price = "";
            this.item.ProductionDate = "";
            $("#categoryId").select2().val("").trigger("change");
            $("#colorId").select2().val("").trigger("change");
        },
        postGet: function () {
            $("#categoryId").select2().val(item.CategoryId).trigger("change");
            $("#colorId").select2().val(item.ColorId).trigger("change");
        },
        validate: function () {
            $("#form").bootstrapValidator({
                message: '商品验证未通过',
                fields: {
                    name: {
                        validators: {
                            notEmpty: {
                                message: '商品名称不能为空'
                            }
                        }
                    },
                    categoryId: {
                        validators: {
                            notEmpty: {
                                message: '商品类别不能为空'
                            }
                        }
                    },
                    colorId: {
                        validators: {
                            notEmpty: {
                                message: '颜色不能为空'
                            }
                        }
                    },
                    price: {
                        validators: {
                            notEmpty: {
                                message: '价格不能为空'
                            }
                        }
                    },
                    productionDate: {
                        validators: {
                            notEmpty: {
                                message: '生产日期不能为空'
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
        $("#queryCategoryId").select2().on("change", function (e) { main.queryEntity.categoryId = $("#queryCategoryId").val(); });
        $("#queryColorId").select2().on("change", function (e) { main.queryEntity.colorId = $("#queryColorId").val(); });
        this.query(1);
    },
    data: {
        queryEntity: {
            name: '',
           categoryId: '',
           colorId: "",
            price: "",
            productionDate: ""
        },
        list: {},
        queryUrl: "/Product/GetList"
    },
    events: {
        'onResetSearch': function () {
            this.queryEntity.name = '';
            this.queryEntity.categoryId = '';
            this.queryEntity.colorId = "";
            this.queryEntity.price = "";
            this.queryEntity.productionDate = "";
            $("#queryCategoryId").select2().val("").trigger("change");
            $("#queryColorId").select2().val("").trigger("change");
        }
    }
});


$("#categoryId").select2().on("change", function (e) { main.$children[1].$children[0].item.CategoryId = $("#categoryId").val(); });
$("#colorId").select2().on("change", function (e) { main.$children[1].$children[0].item.ColorId = $("#colorId").val(); });
