﻿var indexMixin = {
    el: "#main",
    events: {
        'onAddItem': function (title) {
            $("#addItemModal").modal("show");
            this.$broadcast("onAddItem", title);
        },
        'onUpdateItem': function (title, id) {
            $("#addItemModal").modal("show");
            this.$broadcast("onUpdateItem", title, id);
        },
        'onDeleteItem':function(name, id) {
            this.$broadcast("onDeleteItem", name, id);
        },
        'onChange': function (index) {
            this.query(index);
        }
    },
    methods: {
        query: function (index) {
            var $this = this;
            $.get($this.queryUrl + "?pageSize=20&pageIndex=" + index, $this.queryEntity , function (e) {
                $this.list = e.Data;
                $this.$broadcast("onQuery", $this.list);
            });
        }
    }
}


var itemMixin = {
    template: '#formEdit',
    props: ['id','title'],
    ready: function () {
        var $this = this;
        $this.validate();
        $('#addItemModal').on('hide.bs.modal', function () {
            $(form).data('bootstrapValidator').resetForm();
            $("#unknownError").show().find(".help-block").html("");
        });
    },
    events: {
        'onAddItem': function (title) {
            this.clearItem();
            this.editModel = false;
            this.title = title;
        },
        'onUpdateItem': function ( title,id) {
            this.editModel = true;
            this.title = title;
        }
    },
    methods: {
        submit: function (url) {
            var $this = this;
            $(form).data('bootstrapValidator').validate();
            if ($(form).data('bootstrapValidator').isValid()) {
                $.post(url, $this.item, function (e) {
                    if (e.Status === 0) {
                        $("#addItemModal").modal("hide");
                        $this.$dispatch('onChange',1);
                    } else {
                        $("#unknownError").show().find(".help-block").html(e.Message);
                    }
                });
            }
        },
        get: function (url) {
            var $this = this;
            $.get(url, function (e) {
                $this.item = e.Data;
            });
        }
    }
}
