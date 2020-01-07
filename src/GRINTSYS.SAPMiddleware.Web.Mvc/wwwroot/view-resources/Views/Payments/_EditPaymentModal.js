(function ($) {


    var _$modal = $('#PaymentEditModal');
    var _$form = $('form[name=PaymentEditForm]');

    function save() {

        if (!_$form.valid()) {
            return;
        }

        if (!validPaymentAmount()) {
            abp.message.error('The payment amount is greater than the total of the selected invoice(s)', 'Error');
            return;
        }

        var payment = _$form.serializeFormToObject(); //serializeFormToObject is defined in main.js
        payment.invoices = [];
        var _$invoiceCheckboxes = $("input[name='paymentItem']:checked");
        if (_$invoiceCheckboxes) {
            for (var invoiceIndex = 0; invoiceIndex < _$invoiceCheckboxes.length; invoiceIndex++) {
                var _$invoiceCheckbox = $(_$invoiceCheckboxes[invoiceIndex]);
                payment.invoices.push(_$invoiceCheckbox.val());
            }
        }

        abp.ui.setBusy(_$modal);
        abp.ajax({
            url: '/api/services/app/Payment/UpdatePayment',
            data: JSON.stringify(payment)
        }).done(function (data) {
            abp.notify.success('Updated payment with id = ' + data.id);
            _$modal.modal('hide');
            location.reload(true); //reload page to see edited user!
        }).always(function () {
            abp.ui.clearBusy(_$modal);
        });
    }

    function fixedPaymentAmount() {

        var total = getSelectedInvoicesTotalAmount();
        var payedAmount = Number($('#PayedAmount').val());

        if (payedAmount <= 0 || payedAmount > total) {
            $('#PayedAmount').val(total.toFixed(2));
        }

    }

    function validPaymentAmount() {

        var total = getSelectedInvoicesTotalAmount();
        var payedAmount = Number($('#PayedAmount').val());

        if (payedAmount <= 0 || payedAmount > total) {
            return false;
        }

        return true;
    }

    function getSelectedInvoicesTotalAmount() {

        var total = 0.00;

        $("input[name='paymentItem']:checked").each(function () {
            var balanceDue = $(this).parent().parent().parent().find('td').eq(2).html();
            balanceDue = balanceDue.replace("L", "").replace(",", "");
            total = total + Number(balanceDue);
        });

        return Number(total.toFixed(2));
    }

    //Handle save button click
    _$form.closest('div.modal-content').find(".save-button").click(function (e) {
        e.preventDefault();
        save();
    });

    //Handle enter key
    _$form.find('input').on('keypress', function (e) {
        if (e.which === 13) {
            e.preventDefault();
            save();
        }
    });

    $.AdminBSB.input.activate(_$form);

    _$modal.on('shown.bs.modal', function () {
        _$form.find('input[type=text]:first').focus();
    });

    $("#button1").click(function () {
        fixedPaymentAmount();
    });

})(jQuery);