import React from 'react';
import { useBlogPosts } from '../services/api';
import BlogList from '../components/BlogList';
import LoadingSpinner from '../components/LoadingSpinner';
import SEO from '../components/SEO';

const BlogListingPage: React.FC = () => {
  const { data: posts, isLoading, error } = useBlogPosts();

  if (isLoading) {
    return <LoadingSpinner />;
  }

  if (error) {
    return <p className="error-message">Failed to fetch blog posts.</p>;
  }

  return (
    <div>
      <SEO title="All Blog Posts" description="Browse all blog posts from TechCorp Content Hub" />
      <h1>All Blog Posts</h1>
      <BlogList posts={posts || []} />
    </div>
  );
};

export default BlogListingPage;
