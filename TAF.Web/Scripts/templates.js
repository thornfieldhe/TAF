//新增按钮
Vue.component('add-button', {
    props: ['title','item'],
    template: '#addButton',
    methods: {
        newItem: function (title) {
            $("#addItemModal").modal("show");
            this.$dispatch('onAddItem', title);
        }
    }
});

//行搜索按钮
Vue.component('row-search', {
    props: ['index', 'filter', 'model'],
    template: '#searchButton',
    methods: {
        search: function (index, filter, model) {
            var $this = this;
            console.log(index, filter, model);
            taf.model.get(index, filter, model, function (result) { $this.$dispatch('onBindItems', result); });
        },
        resetSearch: function () {
            this.$dispatch('onResetSearch');
        }
    }
});

//行命令按钮
Vue.component('row-command', {
    props: ['id', 'model', 'title', 'name'],
    template: '#scommanButton',
    methods: {
        editItem: function (id, model, title) {
            $("#addUserModal").modal("show");
            this.$dispatch('onUpdateItem', title, id);
        },
        deleteItem: function (id, model, name) {
            taf.delete(id, model, name, function () {
                taf.model.query(1, null, model, function (result) { $this.$dispatch('onBindItems', result); });
            });
        }
    }
});

//编辑页
Vue.component('form-edit', {
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

