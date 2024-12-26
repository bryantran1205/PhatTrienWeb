$(document).ready(function () {
    // Thêm sản phẩm vào giỏ hàng
    $(document).on("click", ".add-to-cart", function (e) {
        e.preventDefault();

        const productId = $(this).data("product-id");
        const productName = $(this).data("product-name");
        const imageUrl = $(this).data("image-url");
        const price = $(this).data("price");

        $.post('/Product/AddToCart', {
            productId: productId,
            productName: productName,
            imageUrl: imageUrl,
            price: price
        }, function (response) {
            if (response.success) {
                console.log(response.message); // Ghi log nếu cần
            }
        });
    });

    // Cập nhật số lượng sản phẩm
    $(document).on("change", ".quantity-input", function () {
        const productId = $(this).data("product-id");
        const quantity = $(this).val();

        if (quantity <= 0) {
            alert("Quantity must be greater than 0.");
            $(this).val(1); // Reset về 1 nếu nhập sai
            return;
        }

        $.post('/Product/UpdateQuantity', {
            productId: productId,
            quantity: quantity
        }, function (response) {
            if (response.success) {
                // Cập nhật tổng giá tiền trực tiếp trên giao diện
                $(`#total-price-${productId}`).text(`$${response.newTotal}`);
                $("#cart-subtotal").text(`$${response.subTotal}`);
                $("#cart-grandtotal").text(`$${response.grandTotal}`);
            }
        });
    });
});
