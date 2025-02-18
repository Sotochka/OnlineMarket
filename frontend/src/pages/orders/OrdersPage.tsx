import React from 'react';
import OrderList from '../../components/OrderList';
import { Link } from 'react-router-dom';
import '../../styles/components/OrderList.css';

const OrdersPage: React.FC = () => (
    <div>
        <h2 style={{ marginBottom: '20px' }}>Orders</h2>
        <Link to="/orders/new" className='create-order-btn'>Create Order</Link>
        <OrderList />
    </div>
);

export default OrdersPage;
