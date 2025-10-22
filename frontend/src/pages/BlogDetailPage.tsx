import React from 'react';
import { useParams } from 'react-router-dom';
import { useBlogPost } from '../services/api';
import BlogDetail from '../components/BlogDetail';
import LoadingSpinner from '../components/LoadingSpinner';
import SEO from '../components/SEO';

const BlogDetailPage: React.FC = () => {
  const { slug } = useParams<{ slug: string }>();
  const { data: post, isLoading, error } = useBlogPost(slug || '');

  if (!slug) {
    return <p className="error-message">Blog post slug is missing.</p>;
  }

  if (isLoading) {
    return <LoadingSpinner />;
  }

  if (error) {
    return <p className="error-message">Failed to fetch blog post.</p>;
  }

  if (!post) {
    return <p>Blog post not found.</p>;
  }

  return (
    <div>
      <SEO title={post.title} description={post.summary} image={post.featuredImage} />
      <BlogDetail post={post} />
    </div>
  );
};

export default BlogDetailPage;
