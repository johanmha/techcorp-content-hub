import React, { useEffect, useState } from 'react';
import { BlogPost } from '../types';
import { getBlogPosts } from '../services/api';
import BlogList from '../components/BlogList';
import LoadingSpinner from '../components/LoadingSpinner';
import SEO from '../components/SEO';

const BlogListingPage: React.FC = () => {
  const [posts, setPosts] = useState<BlogPost[]>([]);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const fetchPosts = async () => {
      try {
        const data = await getBlogPosts();
        setPosts(data);
      } catch (err) {
        setError('Failed to fetch blog posts.');
        console.error(err);
      } finally {
        setLoading(false);
      }
    };

    fetchPosts();
  }, []);

  if (loading) {
    return <LoadingSpinner />;
  }

  if (error) {
    return <p className="error-message">{error}</p>;
  }

  return (
    <div>
      <SEO title="All Blog Posts" description="Browse all blog posts from TechCorp Content Hub" />
      <h1>All Blog Posts</h1>
      <BlogList posts={posts} />
    </div>
  );
};

export default BlogListingPage;
