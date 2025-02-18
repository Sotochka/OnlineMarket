import { useEffect, useState } from "react";
import { Link, useParams } from "react-router-dom";
import { getProductByCode, getProductById, Product } from "../api/product";
import '../styles/components/ProductDetail.css';

const ProductDetail: React.FC = () => {
    const { id, code } = useParams<{ id?: string; code?: string }>();
    const [product, setProduct] = useState<Product | null>(null);
  
    useEffect(() => {
      const fetchProduct = async () => {
          if (id) {
              getProductById(Number(id)).then(setProduct);
          } else if (code) {
            getProductByCode(Number(code)).then(setProduct);
          }
      };
  
      fetchProduct();
    }, [id, code]);
  
    return (
    <div key={product?.id} className="product-card-detail">
        <div className="product-header">
            <div>
                <h2>Product # {product?.id}</h2>
                <Link to={`/products/${product?.id}`} className="product-link">Update product</Link>
            </div>
        </div>
        <div className="product-details">
            <p><strong>Code:</strong> {product?.code}</p>
            <p><strong>Name:</strong> {product?.name}</p>
            <p><strong>Price:</strong> ${product?.price.toFixed(2)}</p>
        </div>
    </div>
    );
  };
  
  export default ProductDetail;