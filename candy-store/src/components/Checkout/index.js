import React from 'react';

const Checkout = (props) => {
    return (
        <div>
            <form>
                <label htmlFor="name">Name</label>
                <input onChange={props.onChange} name="name"/>
                <label htmlFor="adress">Adress</label>
                <input onChange={props.onChange} name="adress"/>
                <label htmlFor="zipcode">Zip Code</label>
                <input onChange={props.onChange} name="zipcode"/>
                <label htmlFor="city">City</label>
                <input onChange={props.onChange} name="city"/>
                <label htmlFor="country">Country</label>
                <input onChange={props.onChange} name="country"/>
                <button onClick={props.onSubmitOrder}>CHECK OUT</button>
            </form>
        </div>
    );
};

export default Checkout;