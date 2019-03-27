import React, { Component } from 'react';
import './App.css';
import Product from '../Product/';

class App extends Component {
  
  state = {
    products: [],
    url: 'http://localhost:7330/api/product/',
    postUrl: 'http://localhost:7330/api/product/',
    cartUrl: 'http://localhost:7330/api/cart/',
    cartId: 0,
    cart: 
    {
      products: [] 
    }
  }
  

  
  componentDidMount() {
    this.setState({
      cartId: this.checkIfCartExists(),
      cart: this.getCart()
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
      const cartUrl = `${this.state.cartUrl}/${this.state.cartId}`;
      console.log(cartUrl);
      fetch(cartUrl)
      .then(response => response.json())
      .then(json => {
        return (json);
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

  render() {
    return (
      <div className="App">
        <header className="App-header">
          
        </header>
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
    );

  }
}

export default App;
