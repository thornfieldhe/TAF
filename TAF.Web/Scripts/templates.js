﻿//新增按钮
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
            this.$broadcast('onSaveItem', this.id);
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
                    $this.$dispatch('onChange');
                    $("#deleteItemDialog").modal("hide");
                } else {
                    taf.notify.danger(e.Message);
                }
            });
            
        }
    }
});