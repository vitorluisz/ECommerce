// Menu

function submitForm(table) {
    table.parentNode.submit();
}

function HomedecreaseQuantity(productId) {
    var quantityInput = document.querySelector(`input[data-product-id="${productId}"]`);
    var currentQuantity = parseInt(quantityInput.value);
    if (currentQuantity > 1) {
        quantityInput.value = currentQuantity - 1;
    }
}

function HomeincreaseQuantity(productId) {
    var quantityInput = document.querySelector(`input[data-product-id="${productId}"]`);
    var currentQuantity = parseInt(quantityInput.value);
    quantityInput.value = currentQuantity + 1;
}

//basket

function BasketdecreaseQuantity(productId, index) {
    var quantityInput = document.querySelector(`input[name="productQuantities[${index}].Quantity"]`);
    var currentQuantity = parseInt(quantityInput.value);
    if (currentQuantity > 1) {
        quantityInput.value = currentQuantity - 1;
        updateFinalizarCompraForm(productId, index, currentQuantity - 1);
    }
}

function BasketincreaseQuantity(productId, index) {
    var quantityInput = document.querySelector(`input[name="productQuantities[${index}].Quantity"]`);
    var currentQuantity = parseInt(quantityInput.value);
    quantityInput.value = currentQuantity + 1;
    updateFinalizarCompraForm(productId, index, currentQuantity + 1);
}

function updateFinalizarCompraForm(productId, index, quantity) {
    document.querySelector(`input[name="productQuantities[${index}].ProductId"]`).value = productId;
    document.querySelector(`input[name="productQuantities[${index}].Quantity"]`).value = quantity;
    recalculateTotal();
}

function recalculateTotal() {
    var total = 0;
    var rows = document.querySelectorAll('table tr');
    rows.forEach(function (row) {
        var quantityInput = row.querySelector('.quantity-input');
        var priceElement = row.querySelector('[data-price]');
        if (quantityInput && priceElement) {
            var quantity = parseInt(quantityInput.value);
            var price = parseFloat(priceElement.getAttribute('data-price').replace('R$', '').replace(',', '.'));
            total += quantity * price;
        }
    });
    document.getElementById('total-value').innerText = `${total.toFixed(2).replace('.', ',')}`;
}