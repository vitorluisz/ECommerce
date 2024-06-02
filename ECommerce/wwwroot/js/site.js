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
        updateQuantityOnServer(productId, currentQuantity - 1);
    }
}

function BasketincreaseQuantity(productId, index) {
    var quantityInput = document.querySelector(`input[name="productQuantities[${index}].Quantity"]`);
    var currentQuantity = parseInt(quantityInput.value);
    quantityInput.value = currentQuantity + 1;
    updateFinalizarCompraForm(productId, index, currentQuantity + 1);
    updateQuantityOnServer(productId, currentQuantity + 1);
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
            var price = parseFloat(priceElement.getAttribute('data-price').replace('R$', '').replace('.', '').replace(',', '.'));
            total += quantity * price;
        }
    });
    document.getElementById('total-value').innerText = `R$ ${total.toFixed(2).replace('.', ',')}`;
}

function updateQuantityOnServer(productId, quantity) {
    $.ajax({
        url: '/Basket/UpdateQuantity',
        type: 'POST',
        data: {
            productId: productId,
            quantity: quantity
        },
        success: function (response) {
            // Você pode adicionar lógica adicional aqui se necessário
        },
        error: function (error) {
            console.error('Erro ao atualizar quantidade no servidor:', error);
        }
    });
}

function finalize() {
    var form = document.getElementById("finalizar-compra-form"); // selecione o formulário correto
    form.submit(); // submeta o formulário
}

function delProductBasket(button) {
    event.preventDefault();
    var productId = button.getAttribute('data-product-id');
    var form = document.getElementById('del-product-form');
    var input = document.createElement('input');
    input.type = 'hidden';
    input.name = 'productId';
    input.value = productId;
    form.appendChild(input);
    form.submit();
}