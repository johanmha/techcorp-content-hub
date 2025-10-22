import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import { Category } from '../types';
import { getCategoryBySlug } from '../services/api';
import LoadingSpinner from '../components/LoadingSpinner';
import SEO from '../components/SEO';

const CategoryPage: React.FC = () => {
  const { slug } = useParams<{ slug: string }>();
  const [category, setCategory] = useState<Category | null>(null);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    if (!slug) {
      setError('Category slug is missing.');
      setLoading(false);
      return;
    }

    const fetchCategory = async () => {
      try {
        const data = await getCategoryBySlug(slug);
        setCategory(data);
      } catch (err) {
        setError('Failed to fetch category details.');
        console.error(err);
      } finally {
        setLoading(false);
      }
    };

    fetchCategory();
  }, [slug]);

  if (loading) {
    return <LoadingSpinner />;
  }

  if (error) {
    return <p className="error-message">{error}</p>;
  }

  if (!category) {
    return <p>Category not found.</p>;
  }

  return (
    <div>
      <SEO title={category.name} description={category.description} />
      <h1 style={{ color: category.color }}>Category: {category.name}</h1>
      <p>{category.description}</p>
      {/* Potentially list blog posts in this category */}
    </div>
  );
};

export default CategoryPage;
