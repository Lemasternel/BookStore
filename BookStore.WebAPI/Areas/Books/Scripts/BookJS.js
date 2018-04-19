    var bookForm = {
        id: $('#Id'),
        title: $('#Title'),
        description: $('#Description'),
        price: $('#Price'),
        quantity: $('#Quantity'),
        publisher: $('#PublisherId'),
        author: $('#AuthorsIds'),

        functions: {
            clearForm: function () {
                bookForm.id.val('0');
                bookForm.title.val('');
                bookForm.description.val('');
                bookForm.price.val('');
                bookForm.quantity.val('');
                bookForm.publisher.find('option').removeProp('selected');
                bookForm.author.find('option').removeProp('selected');
            },
            changePublisher: function (publisher) {
                bookForm.publisher.find('option[value="' + publisher.Id + '"]').prop('selected', 'selected');
            },
            changeAuthors: function (authors) {
                bookForm.author.find('option').removeProp('selected');
                $(authors).each(function (index, author) {
                    bookForm.author.find('option[value="' + author.Id + '"]').prop('selected', 'selected');
                });
            },
            generateSelectPublishers: function (publishers) {
                var options = '';
                $(publishers).each(function (index, res) {
                    options += '<option value="' + res.Id + '">' + res.Name + '</option>';
                });

                bookForm.publisher.empty();
                bookForm.publisher.append(options);
            },
            generateSelectAuthors: function (authors) {
                var options = '';
                $(authors).each(function (index, res) {
                    options += '<option value="' + res.Id + '">' + res.FirstName + " " + res.LastName + '</option>';
                });

                bookForm.author.empty();
                bookForm.author.append(options);
            }
        }
};

    var bookList = {
        container: $('#list'),
        sketch: $('#sketch'),
        table: $('#bookTable')
    }

    function loadBookList() {
        $.get('http://localhost:50026/book/all', function (result) {
            bookList.table.find('.removable-tr').remove();
            $(result).each(function (index, obj) {
                var newTr = bookList.sketch.clone();

                newTr.find('#title').html(obj.Title);
                newTr.find('#description').html(obj.Description);
                newTr.find('#price').html(obj.Price);
                newTr.find('#quantity').html(obj.Quantity);
                newTr.find('button').attr('data-id', obj.Id);
                newTr.addClass('removable-tr');

                newTr.insertBefore(bookList.sketch);

                newTr.removeClass('hidden');
                newTr.removeAttr('id');
            });
        });
    }

    function loadPublishers() {
        $.get('http://localhost:50026/publisher/all', function (result) {
            loadAuthors();
            bookForm.functions.generateSelectPublishers(result);
        });
    };

    function loadAuthors() {
        $.get('http://localhost:50026/author/all', function (result) {
            loadBookList();
            bookForm.functions.generateSelectAuthors(result);
        });
    };

    function createEdit() {
        $('#btnSave').on('click', function () {
            var data = $('#form0').serializeArray();
            var method = 'add';
            if (Number(bookForm.id.val())!= 0) {
                method = 'edit';
            }

            $.ajax({
                url: 'http://localhost:50026/book/' + method,
                data: data,
                type: 'post',
                success: function (result) {
                    loadBookList();
                    bookForm.functions.clearForm();
                    alert(msgSuccess);
                },
                error: function (result) {
                    alert(result.responseJSON.Message);
                }
            });
        });
    };

    function edit(btn) {
        var id = $(btn).data('id');
        $.get('http://localhost:50026/book/get?id=' + id, function (result) {
            bookForm.id.val(result.Id);
            bookForm.title.val(result.Title);
            bookForm.description.val(result.Description);
            bookForm.price.val(result.Price);
            bookForm.quantity.val(result.Quantity);

            bookForm.functions.changePublisher(result.Publisher);
            bookForm.functions.changeAuthors(result.Authors);
        });
    };

    function remove(btn) {
        var id = $(btn).data('id');
        var result = confirm(msgDelete);
        if (result) {
            $.ajax({
                url: 'http://localhost:50026/book/remove',
                data: { id },
                type: 'post',
                success: function (result) {
                    loadBookList();
                },
                error: function (result) {
                    alert(result.responseJSON.Message);
                }
            });
        }
    };

    function validateNumbers() {
        bookForm.quantity.on('keyup', function (event) {
            keyEventValidate(event, bookForm.quantity);
        });
        bookForm.price.on('keyup', function (event) {
            keyEventValidate(event, bookForm.price);
        });
    }

    function keyEventValidate(event, field) {
        if (!$.isNumeric(event.key)) {
            var originalValue = field.val().slice(0, -1);
            field.val(originalValue);
        }
    }

    validateNumbers();
    loadPublishers();
    createEdit();