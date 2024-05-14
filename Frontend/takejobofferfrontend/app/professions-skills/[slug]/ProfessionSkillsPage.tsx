"use client"

import Title from "antd/es/typography/Title";
import { useEffect, useState } from "react";
import { getProfessionSkillsWithNames } from "../../services/professionSkills";
import { ProfessionSkills } from "../../components/ProfessionSkills";
import { getProfessionBySlug } from "@/app/services/professions";

interface ProfessionSkillsProps {
  slug:  string;
}

export default function ProfessionSkillsClient({ slug }: ProfessionSkillsProps) {
    const [professionSkills, setProfessionSkills] = useState<ProfessionSkillWithName[]>([]);
    const [profession, setProfession] = useState<Profession>();
    const [loading, setLoading] = useState(true);
  
    useEffect(() => {
      const getSkillsByProfessionSlug = async () => {
          const professionWithSlug = await getProfessionBySlug(slug);
          const professionSkills = await getProfessionSkillsWithNames(professionWithSlug.id, true);
          
          setProfession(professionWithSlug);
          setProfessionSkills(professionSkills);
          setLoading(false);
      }
      
      getSkillsByProfessionSlug();
    }, []);
  
    return (
      <div>
        {
          loading ? (
            <Title>Loading...</Title>
          ) : (
            <ProfessionSkills values={professionSkills} />
          )
        }
      </div>
    );
}