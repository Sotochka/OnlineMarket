import React from 'react';
import { Link } from 'react-router-dom';
import '../styles/components/NavBar.css';

const Navbar: React.FC = () => {
    return (
        <nav className='navbar'>
            <h1>Order & Product Management</h1>
            <ul className='nav-links'>
                <li><Link to="/" className='nav-link'>Home</Link></li>
                <li><Link to="/orders" className='nav-link'>Orders</Link></li>
                <li><Link to="/products" className='nav-link'>Products</Link></li>
                <li><Link to="/products/new" className='nav-link'>Create Product</Link></li>
                <li><Link to="/orders/new" className='nav-link'>Create Order</Link></li>
            </ul>
        </nav>
    );
};


export default Navbar;
