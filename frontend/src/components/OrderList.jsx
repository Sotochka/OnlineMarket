import React, { useEffect, useState } from 'react';
import { fetchOrders } from '../api';

const OrderList = () => {
    const [orders, setOrders] = useState([]);

    useEffect(() => {
        const getOrders = async () => {
            const data = await fetchOrders();
            setOrders(data);
        };
        getOrders();
    }, []);

    return (
        <div>
            <h2>Order List</h2>
            <ul>
                {orders.map(order => (
                    <li key={order.id}>
                        Order ID: {order.id} - Customer: {order.customerFullName}
                    </li>
                ))}
            </ul>
        </div>
    );
};

export default OrderList;
