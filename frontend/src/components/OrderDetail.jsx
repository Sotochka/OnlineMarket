import React, { useEffect, useState } from 'react';
import { fetchOrderById } from '../api';
import { useParams } from 'react-router-dom';

const OrderDetail = () => {
    const { id } = useParams();
    const [order, setOrder] = useState(null);

    useEffect(() => {
        const getOrder = async () => {
            const data = await fetchOrderById(Number(id));
            setOrder(data);
        };
        getOrder();
    }, [id]);

    if (!order) return <div>Loading...</div>;

    return (
        <div>
            <h2>Order Detail</h2>
            <p>Order ID: {order.id}</p>
            <p>Customer: {order.customerFullName}</p>
            <p>Phone: {order.customerPhone}</p>
            <p>Total Price: ${order.totalPrice}</p>
            {/* Add more details as needed */}
        </div>
    );
};

export default OrderDetail;
