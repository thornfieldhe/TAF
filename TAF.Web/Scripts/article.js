Vue.component('form-body', {
    mixins: [itemMixin],
    template: '#articleFormBody',
    data: function () {
        return {
            item: {
                Id: '00000000-0000-0000-0000-000000000000',
                Title:'',
                PublishDate:'',
                Content:'',
                CategoryId:''
            }
        };
    },
    events: {
        'onSaveItem': function () {
            this.submit("/Article/Save");
        },
        'onGetItem': function (id) {
            this.get("/Article/Get?id=" + id);
        }
    },
    methods: {
        clearItem: function () {
            this.item.Id='00000000-0000-0000-0000-000000000000';
            this.item.Title='';
            this.item.PublishDate='';
            this.item.Content='';
            this.item.CategoryId='';
            $("#categoryId").select2().val("").trigger("change");
        },
        postGet: function () {
            $("#categoryId").select2().val(this.item.CategoryId).trigger("change");
        },
        validate: function () {
            $("#form").bootstrapValidator({
                message: '文章验证未通过',
                fields: {
                    publishDate: {
                        validators: {
                            notEmpty: {
                                message: 'PublishDate不能为空'
                            }
                        }
                    },
                    categoryId: {
                        validators: {
                            notEmpty: {
                                message: 'CategoryId不能为空'
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
                title:'',
                publishDateFrom:'',
                publishDateTo:'',
                content:'',
                categoryId:''
        },
        list: {},
        queryUrl: "/Article/GetList"
    },
    events: {
        'onResetSearch': function () {
            this.queryEntity.title='';
            this.queryEntity.publishDateFrom='';
            this.queryEntity.publishDateTo='';
            this.queryEntity.content='';
            this.queryEntity.categoryId='';
            $("#queryPublishDate").val("");
            $("#queryCategoryId").select2().val("").trigger("change");
        }
    },
    methods: {
        preQuery:function() {
            var dates0 = _.map($("#queryPublishDate").val().split("-"), _.trim);
            this.queryEntity.PublishDateFrom = dates0[0];
            this.queryEntity.PublishDateTo = dates0[1];

        }
    }
});

//搜索栏初始化
$('#queryPublishDate').daterangepicker(datepickerConfig, function (start, end, label) {
    main.queryEntity.publishDateFrom = start.format('YYYY-MM-DD');
    main.queryEntity.publishDateTo = end.format('YYYY-MM-DD');
});
$('#queryPublishDate').next().click(function () {
    $(this).prev().click();
});
$("#queryCategoryId").select2().on("change", function (e) { main.queryEntity.categoryId = $("#queryCategoryId").val(); });


//对象编辑页初始化
var productionDateConfig = $.extend({}, datepickerConfig, { "singleDatePicker": true});
$('#publishDate').daterangepicker(productionDateConfig, function (start, end, label) {
    main.$children[3].$children[0].item.PublishDate = start.format('YYYY-MM-DD');
});
$('#publishDate').next().click(function () {
    $(this).prev().click();
});
$('#publishDate').on('apply.daterangepicker', function (e) {
    $(form).data('bootstrapValidator').updateStatus('publishDate', 'NOT_VALIDATED', null).validateField('publishDate');
});
$("#categoryId").select2().on("change", function (e) { main.$children[3].$children[0].item.CategoryId = $("#categoryId").val(); });
