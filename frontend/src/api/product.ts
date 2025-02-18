import axios from 'axios';

export interface Product {
    id?: number;
    code: string;
    name: string;
    price: number;
}

export interface UpdateProduct {
    code: string;
    name: string;
    price: number;
}

const API_URL = 'http://localhost:5204/api/products';

export const getProducts = async (): Promise<Product[]> => {
    const response = await axios.get(API_URL);
    return response.data.value;
};

export const getProductById = async (id: number): Promise<Product> => {
    const response = await axios.get(`${API_URL}/id/${id}`);
    return response.data.value;
};

export const getProductByCode = async (code: number): Promise<Product> => {
    const response = await axios.get(`${API_URL}/code/${code}`);
    return response.data.value;
}

export const updateProduct = async (product: UpdateProduct, id: number): Promise<Product> => {
    const response = await axios.put(`${API_URL}/${id}`, product);
    return response.data.value;
}

export const createProduct = async (product: Product): Promise<Product> => {
    const response = await axios.post(API_URL, product);
    return response.data.value;
};
