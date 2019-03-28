import React, { Component } from 'react';
import './App.css';
import Product from '../Product/';
import Cart from '../Cart';
import Checkout from '../Checkout';

class App extends Component {
  
  state = {
    products: [],
    url: 'http://localhost:7330/api/product/',
    postUrl: 'http://localhost:7330/api/product/',
    cartUrl: 'http://localhost:7330/api/cart/',
    orderUrl: 'http://localhost:7330/api/order/',
    cartId: 0,
    cart: 
    {
      products: [] 
    },
    order: {
      cart: {},
      customer: {
        name: '',
        adress: '',
        zipcode: '',
        city: '',
        country: ''
      }
    }
  }
  

  
  componentDidMount() {
    this.setState({
      cartId: this.checkIfCartExists(),
    }, () => {
      this.getCart();
    })

    // FETCHING ALL PRODUCTS
    fetch(this.state.url)
    .then(response => response.json())
    .then(json => {
      this.setState({
        products: json
      });
    });
  }
  
  getCart = () => {
    if (this.state.cartId > 0) {
      const cartUrl = `${this.state.cartUrl}${this.state.cartId}`;
      fetch(cartUrl)
      .then(response => response.json())
      .then(json => {
        this.setState({
          cart: json
        })
        
      });
    
    }

  }
  checkIfCartExists = () => {
    let cartId = localStorage.getItem('cartId');
    if (cartId === null){
      return 0;
    }
    
    return cartId;
  };

  // handleChange = (e) => {
  //   this.setState({
  //     order: {
  //         customer: {
  //           [e.target.name]: e.target.value,
  //       }
  //     }
  //   })
  // }

  onSubmitOrder = (e) => {
    e.preventDefault();
    console.log(e);
    this.setState({
      order: {
        customer: {
          [e.target.name]: e.target.value,
        }
      }
      
    }, () => {
      this.placeOrder()
    });
    console.log(this.state)
  }
  placeOrder = () => {
    let order = {
      cart: this.state.cart,
      customer: this.state.customer
    }
    console.log(order);
    fetch(this.state.orderUrl, {
      method: 'POST',
      headers: {
          'Accept': 'application/json',
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(
          {
            cart: this.state.cart, 
            customer: this.state.customer
          })
    })
  }
  render() {
    return (
      <div className="App">
        <header className="App-header">
          
        </header>
        <div>
          <h2>Our Products</h2>
        {
          this.state.products.map((product, i) => {
            return <Product 
            name={product.name} 
            description={product.description} 
            imageurl={product.imageUrl} 
            productId={product.id} 
            key={i}
            cartId={this.state.cartId}/>
          })
        }
        </div>
        
        <div> 
          <Cart cart={this.state.cart} />
          <Checkout onChange={this.handleChange} onSubmitOrder={this.onSubmitOrder}/>

        </div>
        
      </div>
    );
  }
}

export default App;
