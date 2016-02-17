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
    template: '#formEdit',
    props: ['id', 'title'],
    ready: function () {
        var $this = this;
        $('#addItemModal').on('hide.bs.modal', function () {
            $(form).data('bootstrapValidator').resetForm();
            $("#unknownError").show().find(".help-block").html("");
            $this.$broadcast('onClearItem');
        });
    },
    events: {
        'onAddItem': function (title) {
            this.title = title;
        },
        'onUpdateItem': function (title, id) {
            this.title = title;
            this.id = id;
        }
    },
    methods: {
        saveItem: function () {
            this.$broadcast('onSaveItem', this.id);
        }
    }
});

