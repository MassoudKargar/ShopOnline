function MakeUpdateQtyButtonVisible (id, visible) {
    const updateQtyButton = document.querySelector("button[data-itemId='" + id + "']");

    if (visible == true) {
        updateQtyButton.style.display = "inline-block";
    }
    else {
        updateQtyButton.style.display = "none";
    }


}

function initPayPalButton () {
    var description = document.querySelector('#smart-button-container #description');
    var amount = document.querySelector('#smart-button-container #amount');

    var purchase_units = [];
    purchase_units[ 0 ] = {};
    purchase_units[ 0 ].amount = {};

    paypal.Buttons({
        style: {
            color: 'gold',
            shape: 'rect',
            label: 'paypal',
            layout: 'vertical',

        },

        createOrder: function (data, actions) {

            purchase_units[ 0 ].description = description.value;
            purchase_units[ 0 ].amount.value = amount.value;

            return actions.order.create({
                purchase_units: purchase_units,
            });
        },

        onApprove: function (data, actions) {
            return actions.order.capture().then(function (orderData) {

                // Full available details
                console.log('Capture result', orderData, JSON.stringify(orderData, null, 2));

                // Show a success message within this page, e.g.
                const element = document.getElementById('paypal-button-container');
                element.innerHTML = '';
                element.innerHTML = '<h3>Thank you for your payment!</h3>';

                // Or go to another URL:  actions.redirect('thank_you.html');

            });
        },

        onError: function (err) {
            console.log(err);
        }
    }).render('#paypal-button-container');
}