import React from 'react';
import styled from 'styled-components';

const CartProductStyled = styled.div`
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

const CartProduct = props => {
    return (
    <CartProductStyled>
        <img className="product-picture" src={props.imageurl} alt="candypicture"/>
        <div>
            <h1>{props.name}</h1>
            <p>{props.description}</p>
            <p>{props.quantity}</p>
        </div>
    </CartProductStyled>
    );
}

export default CartProduct;