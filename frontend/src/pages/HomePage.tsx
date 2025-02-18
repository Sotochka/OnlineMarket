import React from 'react';
import { Link } from 'react-router-dom';
import '../styles/HomePage.css';

const HomePage: React.FC = () => {
    return (
        <div >
            <nav>
                <Link to="/orders" className='order-link'>View Orders</Link><Link to="/products" className='product-link'>View Products</Link>
            </nav>
        </div>
    );
};

export default HomePage;
