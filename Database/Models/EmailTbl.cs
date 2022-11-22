﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models;

[Table("Email")]
public class EmailTbl
{
	[Key]
	public Guid Id { get; set; }

	public Guid TemplateId { get; set; }
	public string Data { get; set; } = string.Empty;

	[ForeignKey("ToAddressesEmailId")]
	public ICollection<EmailAddressTbl> ToAddresses { get; set; } = new List<EmailAddressTbl>();

	[ForeignKey("CCAddressesEmailId")]
	public ICollection<EmailAddressTbl>? CCAddresses { get; set; }

	[ForeignKey("BCCAddressesEmailId")]
	public ICollection<EmailAddressTbl>? BCCAddresses { get; set; }

	public string Subject { get; set; } = string.Empty;
	public string HtmlContent { get; set; } = string.Empty;
	public string PlainTextContent { get; set; } = string.Empty;

	[MaxLength(5)]
	public string Language { get; set; } = "en-GB";

	public string? HangfireId { get; set; }
	public DateTime? Sent { get; set; }

	// Relationship
	public Guid ProjectId { get; set; }

	public ProjectTbl? Project { get; set; }
}