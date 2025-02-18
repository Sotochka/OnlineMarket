import axios from 'axios';

export interface Order {
    id?: number;
    createdAt: Date;
    customerFullName: string;
    customerPhone: string;
    totalPrice: number;
    products: OrderProduct[];
}

export interface CreateOrder {
    userId: number;
    customerFullName: string;
    customerPhone: string;
    OrderProducts: OrderProduct[];
}

export interface OrderProduct {
    productId: number;
    productName: string;
    productAmount: number;
}

const API_URL = 'http://localhost:5204/api/orders';

export const getOrders = async (): Promise<Order[]> => {
    const response = await axios.get(API_URL);
    console.log(response.data);
    return response.data.value;
};

export const getOrderById = async (id: number): Promise<Order> => {
    const response = await axios.get(`${API_URL}/${id}`);
    console.log(response.data);
    return response.data.value;
};

export const createOrder = async (order: CreateOrder): Promise<Order> => {
    const response = await axios.post(API_URL, order);
    return response.data.value;
};
