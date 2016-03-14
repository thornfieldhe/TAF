Vue.component('form-body', {
    mixins: [itemMixin],
    template: '#productFormBody',
    data: function () {
        return {
            item: {
                Id: '00000000-0000-0000-0000-000000000000',
                Name: '',
                CategoryId: '',
                ColorId: '',
                Price: '',
                ProductionDate: ''
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
            this.item.Id = '00000000-0000-0000-0000-000000000000';
            this.item.Name = '';
            this.item.CategoryId = '';
            this.item.ColorId = '';
            this.item.Price = '';
            this.item.ProductionDate = '';
            $("#categoryId").select2().val("").trigger("change");
            $("#colorId").select2().val("").trigger("change");
            $('#itemPrice').spinbox('value', 0);
        },
        postGet: function () {
            $("#categoryId").select2().val(this.item.CategoryId).trigger("change");
            $("#colorId").select2().val(this.item.ColorId).trigger("change");
            $('#itemPrice').spinbox('value', this.item.Price);
        },
        validate: function () {
            $("#form").bootstrapValidator({
                message: '商品验证未通过',
                fields: {
                    name: {
                        validators: {
                            notEmpty: {
                                message: 'Name不能为空'
                            }
                        }
                    },
                    categoryId: {
                        validators: {
                            notEmpty: {
                                message: 'CategoryId不能为空'
                            }
                        }
                    },
                    price: {
                        validators: {
                            notEmpty: {
                                message: 'Price不能为空'
                            },
                            numeric: {
                                message: 'price必须是数字'
                            },
                            greaterThan: {
                                value: 0,
                                inclusive: false,
                                message: 'price必须大于0'
                            }
                        }
                    },
                    productionDate: {
                        validators: {
                            notEmpty: {
                                message: 'ProductionDate不能为空'
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
            name: '',
            categoryId: '',
            colorId: '',
            price: '',
            productionDateFrom: '',
            productionDateTo: ''
        },
        list: {},
        queryUrl: "/Product/GetList"
    },
    events: {
        'onResetSearch': function () {
            this.queryEntity.name = '';
            this.queryEntity.categoryId = '';
            this.queryEntity.colorId = '';
            this.queryEntity.price = '';
            this.queryEntity.productionDateFrom = '';
            this.queryEntity.productionDateTo = '';
            $("#queryCategoryId").select2().val("").trigger("change");
            $("#queryColorId").select2().val("").trigger("change");
            $('#queryPrice').spinbox('value', 0);
            $("#queryProductionDate").val("");
        }
    },
    methods: {
        preQuery: function () {
            var dates0 = _.map($("#queryProductionDate").val().split("-"), _.trim);
            this.queryEntity.ProductionDateFrom = dates0[0];
            this.queryEntity.ProductionDateTo = dates0[1];

        }
    }
});

//搜索栏初始化
$("#queryCategoryId").select2().on("change", function (e) { main.queryEntity.categoryId = $("#queryCategoryId").val(); });
$("#queryColorId").select2().on("change", function (e) { main.queryEntity.colorId = $("#queryColorId").val(); });
$('#queryPrice').spinbox('value');
$('#queryPrice').on('changed.fu.spinbox', function (e) {
    main.queryEntity.price = $('#queryPrice').spinbox('value');
});
$('#queryProductionDate').daterangepicker(datepickerConfig, function (start, end, label) {
    main.queryEntity.productionDateFrom = start.format('YYYY-MM-DD');
    main.queryEntity.productionDateTo = end.format('YYYY-MM-DD');
});
$('#queryProductionDate').next().click(function () {
    $(this).prev().click();
});


//对象编辑页初始化
var productionDateConfig = $.extend({}, datepickerConfig, { "singleDatePicker": true });
$("#categoryId").select2().on("change", function (e) { main.$children[3].$children[0].item.CategoryId = $("#categoryId").val(); });
$("#colorId").select2().on("change", function (e) { main.$children[3].$children[0].item.ColorId = $("#colorId").val(); });
$('#itemPrice').spinbox();
$('#itemPrice').on('changed.fu.spinbox', function (e) {
    main.$children[3].$children[0].item.Price = $('#itemPrice').spinbox('value');
    $(form).data('bootstrapValidator').updateStatus('price', 'NOT_VALIDATED', null).validateField('price');
});
$('#productionDate').daterangepicker(productionDateConfig, function (start, end, label) {
    main.$children[3].$children[0].item.ProductionDate = start.format('YYYY-MM-DD');
});
$('#productionDate').next().click(function () {
    $(this).prev().click();
});
$('#productionDate').on('apply.daterangepicker', function (e) {
    $(form).data('bootstrapValidator').updateStatus('productionDate', 'NOT_VALIDATED', null).validateField('productionDate');
});








