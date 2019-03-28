import React from 'react';
import CartProduct from '../CartProduct';

const Cart = (props) => {
    return (
        <div>
            <h2>Your Cart:</h2>
            {
                props.cart.products.map((product, i) => {
                    return <CartProduct 
                    name={product.name} 
                    quantity={product.quantity} 
                    imageurl={product.imageUrl} 
                    productId={product.id} 
                    key={i} />
                })
            }
        </div>
    );
};

export default Cart;