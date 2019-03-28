import React, { Component } from 'react';

class AddToCart extends Component {
    state = { 
        cartUrl: 'http://localhost:7330/api/cart/',
        quantity: 0
    };

    handleChange = e => {
        this.setState({
            quantity: e.target.value
        }) 
    }
    handleSubmit = (productId, cartId) => {
        let product = {
            productId: productId,
            quantity: this.state.quantity,
            cartId: cartId
        }
        
        fetch(this.state.cartUrl, {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
            },
            body: JSON.stringify( product )
        }).then(response => response.json())
        .then(json => {
            localStorage.setItem('cartId', JSON.stringify(json.id))
        })
    }
   

    render(props) {
        return (
        <div>
         
                <input onChange={this.handleChange} type="number" name="quantity" value={this.state.quantity} ></input>
                <button type="submit" onClick={() => this.handleSubmit(this.props.productId, this.props.cartId)}>ADD TO CART</button>
           
        </div>
        );
    }
}


export default AddToCart


