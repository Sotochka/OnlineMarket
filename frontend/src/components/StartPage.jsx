import React from 'react';
import { Link } from 'react-router-dom';

const StartPage = () => {
    return (
        <div style={{ textAlign: 'center', marginTop: '50px', background: 'black'}}>
            <h1>Welcome to the Store</h1>
            <p>Select an option below:</p>
            <div>
                <Link to="/orders" style={{ margin: '10px', textDecoration: 'none', color: 'blue' }}>
                    View Orders
                </Link>
                <Link to="/products" style={{ margin: '10px', textDecoration: 'none', color: 'blue' }}>
                    View Products
                </Link>
            </div>
        </div>
    );
};

export default StartPage;
