import React, { useEffect, useState } from 'react';
import { getProducts, Product } from '../api/product';
import { Link } from 'react-router-dom';

const ProductList: React.FC = () => {
    const [products, setProducts] = useState<Product[]>([]);

    useEffect(() => {
        getProducts().then(setProducts);
    }, []);

    return (
        <div className="product-container">
            <div className='product-grid'>
                {products.map(product => (
                    <div key={product.id} className="product-card">
                        <div className="product-header">
                            <div>
                                <h2>Product # {product.id}</h2>
                            </div>
                            <div className='product-links'>
                                <Link to={`/products/code/${product.code}`} className="product-link">Get by Code</Link>
                                <Link to={`/products/id/${product.id}`} className="product-link">Get by Id</Link>
                            </div>
                        </div>
                        <div className="product-details">
                            <p><strong>Code:</strong> {product.code}</p>
                            <p><strong>Name:</strong> {product.name}</p>
                            <p><strong>Price:</strong> ${product.price.toFixed(2)}</p>
                        </div>
                    </div>
                ))}
            </div>
        </div>
    );   
};

export default ProductList;
