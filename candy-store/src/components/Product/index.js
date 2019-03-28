import React from 'react';
import AddToCart from '../AddToCart';
import styled from 'styled-components';

const ProductStyled = styled.div`
    width: 600px;
    height: 210px;
    box-shadow: 4px 4px 8px grey;
    padding: 10px;
    display: grid;
    grid-template-columns: 1fr 1fr;
    margin: 10px;
    grid-gap: 10px;
    img {
        width: 200px;
        height: auto;
        border: 2px solid grey;
        border-radius: 10px;
    }
`;

const Product = props => {
    return (
    <ProductStyled>
        <img className="product-picture" src={props.imageurl} alt="candypicture"/>
        <div>
            <h1>{props.name}</h1>
            <p>{props.description}</p>
            <div>
                <AddToCart productId={props.productId} cartId={props.cartId}/>
            </div>
        </div>
    </ProductStyled>
    );
}

export default Product;