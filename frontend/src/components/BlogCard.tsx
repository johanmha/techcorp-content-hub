import React from 'react';
import { BlogPost } from '../types';
import { Link } from 'react-router-dom';

interface BlogCardProps {
  post: BlogPost;
}

const BlogCard: React.FC<BlogCardProps> = ({ post }) => {
  return (
    <div className="blog-card">
      <Link to={`/blog/${post.slug}`}>
        {post.featuredImage && <img src={post.featuredImage} alt={post.title} />}
        <h2>{post.title}</h2>
      </Link>
      <p>{post.summary}</p>
      {/* Add author, categories, date, etc. */}
    </div>
  );
};

export default BlogCard;
