import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import { BlogPost } from '../types';
import { getBlogPostBySlug } from '../services/api';
import BlogDetail from '../components/BlogDetail';
import LoadingSpinner from '../components/LoadingSpinner';
import SEO from '../components/SEO';

const BlogDetailPage: React.FC = () => {
  const { slug } = useParams<{ slug: string }>();
  const [post, setPost] = useState<BlogPost | null>(null);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    if (!slug) {
      setError('Blog post slug is missing.');
      setLoading(false);
      return;
    }

    const fetchPost = async () => {
      try {
        const data = await getBlogPostBySlug(slug);
        setPost(data);
      } catch (err) {
        setError('Failed to fetch blog post.');
        console.error(err);
      } finally {
        setLoading(false);
      }
    };

    fetchPost();
  }, [slug]);

  if (loading) {
    return <LoadingSpinner />;
  }

  if (error) {
    return <p className="error-message">{error}</p>;
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
