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
                alert(response.message); // Ghi log nếu cần
                window.location.href = '/Cart';
            }
            else {
                aler("Fail to add to cart")
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

    $(document).ready(function () {
        // Xóa sản phẩm khỏi giỏ hàng
        $(document).on("click", ".remove-from-cart", function (e) {
            e.preventDefault();

            const productId = $(this).data("product-id");

            if (confirm("Are you sure you want to remove this item?")) {
                $.post('/Product/RemoveFromCart', { productId: productId }, function (response) {
                    if (response.success) {
                        // Xóa hàng của sản phẩm khỏi bảng
                        $(`#total-price-${productId}`).closest("tr").remove();

                        // Cập nhật lại Subtotal, Tax, và Grand Total
                        updateOrderDetails({
                            subTotal: response.subTotal,
                            tax: response.tax,
                            grandTotal: response.grandTotal
                        });
                    } else {
                        alert("Failed to remove item from cart.");
                    }
                });
            }
        });
    });
    // Hàm cập nhật chi tiết đơn hàng (Order Detail)
    function updateOrderDetails(orderDetails) {
        // Cập nhật Subtotal
        if (orderDetails.subTotal !== undefined) {
            $("#cart-subtotal").text(`$${orderDetails.subTotal.toFixed(2)}`);
        }
        // Cập nhật Tax
        if (orderDetails.tax !== undefined) {
            $("#cart-tax").text(`$${orderDetails.tax.toFixed(2)}`);
        }
        // Cập nhật Grand Total
        if (orderDetails.grandTotal !== undefined) {
            $("#cart-grandtotal").text(`$${orderDetails.grandTotal.toFixed(2)}`);
        }
    }
    function updateCartBadge(count) {
        $('#cart-count').text(count);
    }
    $(document).ready(function () {
        // Mở side menu
        $('.side-menu a').on('click', function (e) {
            e.preventDefault();
            $('.side').addClass('open');
        });

        // Đóng side menu
        $('.close-side').on('click', function () {
            $('.side').removeClass('open');
        });
    });


});
