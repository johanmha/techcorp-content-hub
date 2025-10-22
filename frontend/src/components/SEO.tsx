import React from 'react';
import { Helmet } from 'react-helmet'; // You'll need to install react-helmet

interface SEOProps {
  title: string;
  description?: string;
  name?: string; // Site name
  type?: string; // e.g., 'article', 'website'
  url?: string;
  image?: string;
}

const SEO: React.FC<SEOProps> = ({
  title,
  description,
  name = "TechCorp Content Hub",
  type = "website",
  url = window.location.href,
  image,
}) => {
  return (
    <Helmet>
      <title>{title}</title>
      <meta name="description" content={description} />
      <meta property="og:type" content={type} />
      <meta property="og:title" content={title} />
      <meta property="og:description" content={description} />
      <meta property="og:url" content={url} />
      {image && <meta property="og:image" content={image} />}
      <meta name="twitter:creator" content={name} />
      <meta name="twitter:card" content="summary_large_image" />
      <meta name="twitter:title" content={title} />
      <meta name="twitter:description" content={description} />
      {image && <meta name="twitter:image" content={image} />}
    </Helmet>
  );
};

export default SEO;
