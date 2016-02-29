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
Vue.component('search-command', {
    props: ['index', 'filter', 'model'],
    template: '#searchCommand',
    methods: {
        search: function () {
            this.$dispatch('onChange', 1);
        },
        resetSearch: function () {
            this.$dispatch('onResetSearch');
        }
    }
});

//行命令按钮
Vue.component('row-command', {
    props: ['id', 'title', 'name'],
    template: '#rowCommand',
    methods: {
        editItem: function (id, title) {
            $("#addItemModal").modal("show");
            this.$dispatch('onUpdateItem', title, id);
        },
        deleteItem: function (id, name) {
            this.$dispatch('onDeleteItem', name, id);
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
            this.$broadcast("onNewItem");
        },
        'onUpdateItem': function (title, id) {
            this.title = title;
            this.$broadcast("onGetItem", id);
        }
    },
    methods: {
        saveItem: function () {
            this.$broadcast('onSaveItem');
        }
    }
});

//删除对话框
Vue.component('dialog-delete', {
    template: '#dialogDelete',
    props: ['id', 'name', 'deleteUrl'],
    events: {
        'onDeleteItem': function (name, id) {
            $("#deleteItemDialog").modal("show");
            this.id = id;
            this.name = name;
        }
    },
    methods: {
        deleteItem: function () {
            var $this = this;
            $.post($this.deleteUrl + $this.id, function (e) {
                if (e.Status === 0) {
                    $this.$dispatch('onChange',1);
                    $("#deleteItemDialog").modal("hide");
                } else {
                    taf.notify.danger(e.Message);
                }
            });
        }
    }
});

//表脚
Vue.component('table-foot', {
    template: '#tableFoot',
    props: ['colspan', 'list'],
    data: function() {
        return {
            list: {
            },
            length:0
        }
    },
    events: {
        'onQuery': function (list) {
            console.log(list,33);
            this.list = list;
            this.length = this.list.Datas.length;
        }
    },
    methods: {
        query: function (index) {
            this.$dispatch('onChange', index);
        }
    }
});