import React from 'react';
import { BlogPost } from '../types';

interface BlogDetailProps {
  post: BlogPost;
}

const BlogDetail: React.FC<BlogDetailProps> = ({ post }) => {
  return (
    <article className="blog-detail">
      {post.featuredImage && <img src={post.featuredImage} alt={post.title} />}
      <h1>{post.title}</h1>
      {post.author && <p>By {post.author.name}</p>}
      <div dangerouslySetInnerHTML={{ __html: post.content }} />
      {/* Add categories, tags, published date, etc. */}
    </article>
  );
};

export default BlogDetail;
