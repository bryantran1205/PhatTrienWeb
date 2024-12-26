function addToCart(id) {
	$.ajax({
		type: "POST",
		url: '/addcart/' + id,
		// data: { productId: id },
		success: function () {
			
				console.log("succes")
				
		error: function () {
			console.log("error"l)
		}
	});
}